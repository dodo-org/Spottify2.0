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
}

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
}

#Min.io
resource "docker_network" "spotify_network" {
  name = "spotify_file_servers_network"
}

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
    name = docker_network.spotify_network.name
  }
}

// API

//Loadbalancer / rteverse Proxy


