FROM ubuntu:20.10
EXPOSE 8080
RUN 'mkdir' '/game'
WORKDIR /game
COPY build/StandaloneLinux64/ .
RUN chmod +x ./StandaloneLinux64.x86_64
ENTRYPOINT [ "./StandaloneLinux64.x86_64","-batchmode","-nographics"]