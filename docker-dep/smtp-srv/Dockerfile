FROM alpine:latest
LABEL maintainer="mika@jellyfish.net"

RUN apk update && apk add postfix

COPY ["main.cf", "/etc/postfix/main.cf"]
COPY ["master.cf", "/etc/postfix/master.cf"]

EXPOSE 25 587

CMD ["postfix", "start-fg"]