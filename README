# StaticFileServer
## dotnet core project

This projects contains a bare static file delivery server with configurable base path to be used as a base docker image for simple web applications

## Usage:

### Dockerfile
Create a Dockerfile for your application:

```
FROM registry.gitlab.com/rumpel78/staticfileserver:latest
COPY ./files/ /app/wwwroot
```
Change the COPY line according to your static source files

### docker-compose.yml
Your docker-compose.yml should look like this one:
```
version: '2'

services:
  mySite:    
    build: .
    ports:
      - 80:80
    environment:
      - BASEPATH=/myBasePath
```

Change the basepath according to your wishes.

### Why I build this image

Because I wanted some easy and performant solution of hosting some plain html/js files using Traefik Proxy and Path selector.