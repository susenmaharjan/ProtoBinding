setlocal

set PROTOC=%UserProfile%\.nuget\packages\Google.Protobuf.Tools\3.6.1\tools\windows_x64\protoc.exe


set PersonPath=person.proto

set PersonsPath=persons.proto




set outputPath=./

%PROTOC% -I.\ --csharp_out %outputPath% %PersonPath%
%PROTOC% -I.\ --csharp_out %outputPath% %PersonsPath%
