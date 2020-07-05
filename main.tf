provider "google-beta" {

  project = "lightbikenode"
  region  = "us-central1"
  zone    = "us-central1-a"
}

variable "version" {
    type = string
    description = "tag to deploy"
    required = true
}

module "auto-single-lb" {
  source = "git::github.com/RobertAron/SingleServerGcloud?ref=v0.1"
  image  = "gcr.io/lightbikeunity/game-server:${version}"
  domain = "unity.bike.robertaron.io"
}