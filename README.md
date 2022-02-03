
---
## Build an image

---

#### - Build image  
`docker build -t texiu:1.0.0 --force-rm -f texiu/Dockerfile .`

#### - Save image to a local file  
`docker save texiu > texiu.tar`

#### - Import image to k8s  
`microk8s ctr image import texiu.tar`

#### - List images  
`microk8s ctr images ls "name~=texiu"`

---
## Deploy on server

---
#### - Edit the external ip with the vps public ip in the deployment file 'texiu-k8s.yaml'
```
  externalIPs:
  - 0.0.0.0     # the VPS public IP address
```

#### - Apply deployment  

`microk8s.kubectl apply -f texiu-k8s.yaml`

---
### Check if the apis are working
`curl VPS_public_ip:31101/random/flag?length=10`

---
