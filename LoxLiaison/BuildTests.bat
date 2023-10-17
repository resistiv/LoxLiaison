@echo off
dotnet build
%~dp0\LoxTestGenerator\bin\Debug\net6.0\LoxTestGenerator.exe %~dp0\LoxTester\Tests\ %~dp0\LoxTester\
dotnet build
pause