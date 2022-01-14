#Questions for people

*Q: Should I put things like stats, skills in a dictionary or simply leave them all as individual variables.
A: JP recommends that I keep them in seperte functions, to make the code mroe readable.

*Q: where do i put things like Random and CSV readers and such
A: JP recommended putting them in constructors because tey get dropped after.
However the CSV reader is too much processing for a constructor, instead create it at the begining and store it in memory

Q: How to create error messages that matter.


Q: modifiers inside stats, or as a seperate object?  Should the apply method create and store it?  Or should it be created by the event.


Q: should every function be built with error detection?


Q: should i make all these variables readonly?