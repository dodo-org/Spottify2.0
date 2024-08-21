terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.2"
    }
  }
}

# Definiere ein eigenes Docker-Netzwerk
resource "docker_network" "custom_network" {
  name = "custom_network"
}
#------
# Primary PostgreSQL Server
resource "docker_image" "postgres" {
  name = "postgres:latest"
}

resource "docker_container" "primary_postgres" {
  image = docker_image.postgres.image_id
  name  = "primary_postgres"
  ports {
    internal = 5432
    external = 5432
  }
  env = [
    "POSTGRES_DB=mydb",
    "POSTGRES_USER=user",
    "POSTGRES_PASSWORD=password",
  ]

  # Mount custom postgresql.conf and pg_hba.conf
  volumes {
    host_path      = "${abspath(path.module)}/postgresql.conf"
    container_path = "/etc/postgresql/data/postgresql.conf"
  }
  volumes {
    host_path      = "${abspath(path.module)}/pg_hba.conf"
    container_path = "/etc/postgresql/data/pg_hba.conf"
  }

  networks_advanced {
    name = docker_network.custom_network.name
  }
}

# Replica PostgreSQL Server
resource "docker_container" "replica_postgres" {
 image = docker_image.postgres.image_id
 name  = "replica_postgres"
 env = [
  #  "POSTGRES_DB=mydb",
   "POSTGRES_USER=replicator",
   "POSTGRES_PASSWORD=replicator_password",
 ]
  ports {
   internal = 5432
   external = 5433
  }
#  command = ["/bin/bash", "-c", "rm -rf /etc/postgresql/data/* && pg_basebackup -h primary_postgres -D /etc/postgresql/data -U user -v -P --wal-method=stream"]

  command = ["/docker-entrypoint.sh"]
  entrypoint = [
   "/bin/bash", 
   "-c", 
   <<-EOT
     until pg_isready -h primary_postgres -p 5432 -U replicator; do
       echo "Waiting for primary database..."
       sleep 2
     done
     
     rm -rf /etc/postgresql/data/
     pg_basebackup -h primary_postgres -D /etc/postgresql/data/ -U replicator -v -P --wal-method=stream
     
     echo "primary_conninfo = 'host=primary_postgres port=5432 user=replicator password=replicator_password'" >> /etc/postgresql/data/postgresql.auto.conf
     exec docker-entrypoint.sh postgres
   EOT
  ]

  volumes {
    host_path      = "${abspath(path.module)}/postgresql.conf"
    container_path = "/etc/postgresql/data/postgresql.conf"
  }
  volumes {
   host_path      = "${abspath(path.module)}/pg_hba.conf"
   container_path = "/etc/postgresql/data/pg_hba.conf"
  }


  networks_advanced {
   name = docker_network.custom_network.name
  }
  depends_on = [
   docker_container.primary_postgres
  ]
}


#------
# pgAdmin Container
resource "docker_image" "pgadmin" {
  name = "dpage/pgadmin4:latest"
}

resource "docker_container" "pgadmin" {
  image = docker_image.pgadmin.image_id
  name  = "pgadmin"
  ports {
    internal = 80
    external = 9090
  }

  env = [
    "PGADMIN_DEFAULT_EMAIL=admin@example.com",
    "PGADMIN_DEFAULT_PASSWORD=admin"
  ]

  networks_advanced {
    name = docker_network.custom_network.name
  }

  depends_on = [
    docker_container.primary_postgres
  ]
}
#------
#Redis
resource "docker_image" "redis" {
  name = "redis:latest"
}

resource "docker_container" "redis" {
  name  = "redis"
  image = docker_image.redis.image_id
  ports {
    internal = 6379
    external = 6379
  }

  networks_advanced {
    name = docker_network.custom_network.name
  }
}
#------
#Min.io
resource "docker_image" "minio_image" {
  name = "minio/minio:latest"
}

resource "docker_container" "minio_container" {
  image = docker_image.minio_image.image_id
  name  = "minio"
  env = [
    "MINIO_ROOT_USER=minio",
    "MINIO_ROOT_PASSWORD=minio123"
  ]
  ports {
    internal = 9000
    external = 9000
  }
  command = ["server", "/data"]

  networks_advanced {
    name = docker_network.custom_network.name
  }
}

#------
# Build Docker Image for API
resource "docker_image" "Api_Image" {
  name         = "api_image:latest"
  build {
    context    = "./Spotify_Api"
  }
}

# Deploy Two API Containers
resource "docker_container" "Api_container" {
  count = 2
  image = docker_image.Api_Image.image_id
  name  = "Api_container_${count.index + 1}"
  ports {
    internal = 8080
    external = "${8080 + count.index}"

  }

  networks_advanced {
    name = docker_network.custom_network.name
  }
  
  depends_on = [
    docker_container.primary_postgres
  ]
}
#------
# Load balancer / Reverse Proxy
resource "docker_image" "nginx" {
  name = "nginx:latest"
}

resource "docker_container" "nginx" {
  image = docker_image.nginx.name
  name = "loadbalancer"
  ports {
    internal = 85
    external = 85
  }

    volumes {
      host_path      = "${abspath(path.module)}/nginx.conf"
      container_path = "/etc/nginx/nginx.conf"
  }

  networks_advanced {
    name = docker_network.custom_network.name
  }

  depends_on = [
    docker_container.Api_container
  ]
}


