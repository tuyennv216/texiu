## Build and import  

### - Build image  

`docker build -t texiu:1.0.0 --force-rm -f texiu/Dockerfile .`

### - Save image to a local file  

`docker save hello-web > texiu.tar`

### - Import image to k8s  

`microk8s.ctr image import texiu.tar`

### - List images  

`microk8s.ctr images ls "name~=texiu"`

### - Apply deployment  

`microk8s.kubectl apply -f texiu-k8s.yaml`

## Build to local registry  

### - Build image  

`docker build -t localhost:32000/texiu:1.0.0 --force-rm -f texiu/Dockerfile .`

### - Push  

`docker push localhost:32000/texiu`

## Deploy the image  

`microk8s kubectl apply -f texiu.yaml`

### Create a nodeport  

`microk8s.kubectl expose deployment texiu-deployment --type=NodePort --name=texiu-nodeport`


### Get the port  

`microk8s kubectl get services`

