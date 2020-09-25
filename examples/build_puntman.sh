#!/bin/bash
docker build -t untzakoncrg.azurecr.io/puntman:$1 -f puntman/Dockerfile .
docker push untzakoncrg.azurecr.io/puntman:$1