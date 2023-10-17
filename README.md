# Gas and atmospheric variables simulation

This project was implemented as a home assigment within the OOP course, ELTE, Spring 2022.

## Description

Layers of gases are given, with certain type (ozone, oxygen, carbon dioxide) and thickness, affected by atmospheric variables (thunderstorm, sunshine, other effects).
When a part of one layer changes into another layer due to an atmospheric variable, the newly transformed layer ascends and engrosses the first identical type of layer of gases over it.
In case there is no identical layer above, it creates a new layer at the top of the atmosphere.
The documentation can be found in the PDF format. The Visitor Design Pattern was used for this simulation.
The test cases are also provided.
