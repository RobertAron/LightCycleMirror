provider "google-beta" {

  project = "lightbikeunity"
  region  = "us-central1"
  zone    = "us-central1-a"
}

variable "tag" {
    type = string
    description = "tag to deploy"
}

module "auto-single-lb" {
  source = "git::github.com/RobertAron/SingleServerGcloud?ref=v0.1"
  image  = "gcr.io/lightbikeunity/game-server:${var.tag}"
  domain = "unity.bike.robertaron.io"
}