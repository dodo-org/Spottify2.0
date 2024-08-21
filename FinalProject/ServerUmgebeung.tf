//terraform init
//terraform apply

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
# PostgreSQL
resource "docker_image" "postgres" {
  name = "postgres:latest"
}

resource "docker_container" "postgres" {
  image = docker_image.postgres.image_id
  name  = "postgres"
  ports {
    internal = 5432
    external = 5432
  }
  env = [
    "POSTGRES_DB=mydb",
    "POSTGRES_USER=user",
    "POSTGRES_PASSWORD=password"
  ]

  networks_advanced {
    name = docker_network.custom_network.name
  }
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

  # Verlinken mit dem PostgreSQL-Container
  networks_advanced {
    name = docker_network.custom_network.name
  }

  depends_on = [
    docker_container.postgres
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
    docker_container.postgres
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


