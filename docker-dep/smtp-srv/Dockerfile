FROM alpine:latest
LABEL maintainer="mika@jellyfish.net"

RUN apk update && apk add postfix busybox-extras bind-tools

COPY ["sasl_passwd", "/etc/postfix/sasl_passwd"]
COPY ["main.cf", "/etc/postfix/main.cf"]

RUN postmap /etc/postfix/sasl_passwd
RUN chmod 600 /etc/postfix/sasl_passwd

EXPOSE 25

CMD ["postfix", "start-fg"]
