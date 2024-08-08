terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.2"
    }
  }
}

# Cassandra
resource "docker_image" "cassandra" {
  name = "cassandra:latest"
}

resource "docker_container" "cassandra" {
  image = docker_image.cassandra.image_id
  name  = "cassandra"
  ports {
    internal = 9042
    external = 9042
  }

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
