# CodeUnit

the codeunit is empty.

An empty code unit inherits from NavCodeunit and uses a NavCodeUnitOptions attribute.

The name of the code unit is therefore constructed as follows: Objecttype + ID. for example Codeunit 93000 means Codeunit93000

## Usings

    The usings are within the namespace

* System;
* Microsoft.Dynamics.Nav.Runtime;
* Microsoft.Dynamics.Nav.Types;
* Microsoft.Dynamics.Nav.Types.Exceptions;
* Microsoft.Dynamics.Nav.Common.Language;
* Microsoft.Dynamics.Nav.EventSubscription;

## Namespace

* Microsoft.Dynamics.Nav.BusinessApplication

## Base Class

* NavCodeunit: __ctor__ (parent, _id_)

## Attribute

* NavCodeUnitOptions

## ctor

* ITreeObject parent

## Overrides

* ObjectName: Returns the name of the object as string
* OnClear: do nothing

## Statics

* __Construct(ITreeObject parent): returns new CodeUnit.