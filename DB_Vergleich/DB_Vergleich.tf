terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.2"
    }
  }
}

# MySQL
resource "docker_image" "mysql" {
  name = "mysql:latest"
}

resource "docker_container" "mysql" {
  image = docker_image.mysql.image_id
  name  = "mysql"
  ports {
    internal = 3306
    external = 3306
  }
  env = [
    "MYSQL_ROOT_PASSWORD=rootpassword",
    "MYSQL_DATABASE=mydb",
    "MYSQL_USER=user",
    "MYSQL_PASSWORD=password"
  ]

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

# SQLite
# Note: SQLite does not run as a server, so we use a mock example here.


# MongoDB
resource "docker_image" "mongo" {
  name = "mongo:latest"
}

resource "docker_container" "mongo" {
  image = docker_image.mongo.image_id
  name  = "mongo"
  ports {
    internal = 27017
    external = 27017
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
