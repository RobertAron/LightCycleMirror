FROM ubuntu:20.10
EXPOSE 80
RUN 'mkdir' '/game'
WORKDIR /game
COPY build/StandaloneLinux64/ .
RUN chmod +x ./StandaloneLinux64
ENTRYPOINT [ "./StandaloneLinux64","-batchmode","-nographics"]