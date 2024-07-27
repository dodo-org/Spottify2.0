terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.2"
    }
  }
}

resource "docker_image" "redis" {
  name = "redis:latest"
}

resource "docker_image" "memcached" {
  name = "memcached:latest"
}

resource "docker_image" "hazelcast_host" {
  name = "docker_container.hazelcast.ip_address"
}

resource "docker_container" "redis" {
  name  = "redis"
  image = docker_image.redis.image_id
  ports {
    internal = 6379
    external = 6379
  }
}

resource "docker_container" "memcached" {
  name  = "memcached"
  image = docker_image.memcached.image_id
  ports {
    internal = 11211
    external = 11211
  }
}

resource "docker_container" "hazelcast" {
  name  = "hazelcast"
  image = "hazelcast/hazelcast:latest"
  ports {
    internal = 5701
    external = 5701
  }
}