terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.2"
    }
  }
}

resource "docker_network" "test_network" {
  name = "file_servers_network"
}

resource "docker_image" "minio_image" {
  name = "minio/minio:latest"
}

resource "docker_image" "gluster_image" {
  name = "gluster/gluster-centos:latest"
}

resource "docker_image" "nfs_image" {
  name = "name = erichough/nfs-server:latest"
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
    name = docker_network.test_network.name
  }
}

resource "docker_container" "gluster_container" {
  image = docker_image.gluster_image.image_id
  name  = "gluster"
  privileged = true
  networks_advanced {
    name = docker_network.test_network.name
  }
}

resource "docker_container" "nfs_container" {
  image = docker_image.nfs_image.image_id
  name  = "nfs"
  volumes {
    host_path      = "/path/to/nfs_data"
    container_path = "/nfsshare"
  }
  env = [
    "SHARED_DIRECTORY=/nfsshare"
  ]
  networks_advanced {
    name = docker_network.test_network.name
  }
}