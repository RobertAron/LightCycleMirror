FROM ubuntu:20.10
EXPOSE 80
RUN 'mkdir' '/game'
WORKDIR /game
COPY /Build/StandaloneLinux64/ .
ENTRYPOINT [ "./StandaloneLinux64","-batchmode","-nographics"]