#!/bin/bash

cd `dirname $0`
SCRIPTDIR=`pwd`
cd -

dotnet build
$SCRIPTDIR/LoxTestGenerator/bin/Debug/net6.0/LoxTestGenerator $SCRIPTDIR/LoxTester/Tests/ $SCRIPTDIR/LoxTester/
dotnet build