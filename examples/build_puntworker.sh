#!/bin/bash
docker build -t untzakoncrg.azurecr.io/puntworker:$1 -f puntworker/Dockerfile .
docker push untzakoncrg.azurecr.io/puntworker:$1