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

# pgAdmin Container
#resource "docker_image" "pgadmin" {
#  name = "dpage/pgadmin4:latest"
#}
#
#resource "docker_container" "pgadmin" {
#  image = docker_image.pgadmin.image_id
#  name  = "pgadmin"
#  ports {
#    internal = 80
#    external = 9090
#  }
#
#  env = [
#    "PGADMIN_DEFAULT_EMAIL=admin@example.com",
#    "PGADMIN_DEFAULT_PASSWORD=admin"
#  ]
#
#  # Verlinken mit dem PostgreSQL-Container
#  networks_advanced {
#    name = docker_network.custom_network.name
#  }
#
#  depends_on = [
#    docker_container.postgres
#  ]
#}

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

// API 1

# Bauen des Docker-Images
resource "docker_image" "Api_Image" {
  name         = "api_image:latest"
#  build {
#    context    = "./Spotify_Api"
#  }
}


resource "docker_container" "Api_container" {
  image = docker_image.Api_Image.image_id
  name  = "Api_container"
  ports {
    internal = 8080
    external = 8080
  }

  networks_advanced {
    name = docker_network.custom_network.name
  }
}

#// API 2
#
## Bauen des Docker-Images
#resource "docker_image" "Api_Image2" {
#  name         = "api_image2:latest"
##  build {
##    context    = "./Spotify_Api"
##  }
#}
#
#
#resource "docker_container" "Api_container2" {
#  image = docker_image.Api_Image2.image_id
#  name  = "Api_container2"
#  ports {
#    internal = 8080
#    external = 9090
#  }
#
#  networks_advanced {
#    name = docker_network.custom_network.name
#  }
#}
#
#//Loadbalancer / reverse Proxy
#resource "docker_image" "nginx_image" {
#  name = "nginx:latest"
#}
#
#resource "docker_container" "nginx" {
#  image = docker_image.nginx_image.image_id
#  name = "loadbalancer"
#  ports {
#    internal = 85
#    external = 8000
#  }
#}
#
#
#resource "docker_container" "nginx_load_balancer" {
#  image = docker_image.nginx_image.image_id
#  name  = "nginx_load_balancer"
#
#  ports {
#    internal = 80
#    external = 80
#  }
#
#  networks_advanced {
#    name = docker_network.custom_network.name
#  }
#
#  volumes {
#    container_path = "/etc/nginx/conf.d/default.conf"
#    host_path      = "${abspath(path.module)}/nginx.conf"
#  }
#}
#
#output "load_balancer_ip" {
#  value = docker_container.nginx_load_balancer.network_data[0].ip_address
#}