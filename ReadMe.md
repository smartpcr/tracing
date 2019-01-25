``` cmd
docker run -d --rm -p 6831:6831/udp -p 6832:6832/udp -p 16686:16686 jaegertracing/all-in-one:1.7 --log-level=debug


docker run -d -p 9411:9411 openzipkin/zipkin

```