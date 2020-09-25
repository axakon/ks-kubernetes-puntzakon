# Example 1 - Load balancer

Show YML!!!!

Key points:
* Infrastructure is code based. Everything to run our application inside our cluster is defined inside our deployment
* Removal of deployments is super easy

Deploy simpel only
```
kubectl apply -f deployment_simpel.yml
```

Show website. http://p.untzakon.se
It will crash, show logs.

```
kubectl delete deployments/puntman
```

Show website. http://p.untzakon.se

Add it back again!

# Example 2 - Service discovery

Deploy redis
```
kubectl apply -f deployment_redis.yml
```

Run add example pun script

Show website and persistance. http://p.untzakon.se

# Example 3 - Scaling

Scale simpel
```
kubectl scale --replicas=3 deployments/puntman
```

Show website and chaning GUID's. http://p.untzakon.se

# Example 5 - Enable sending! ;)

```
kubectl apply -f deployment_troll.yml
```

Show website. http://p.untzakon.se