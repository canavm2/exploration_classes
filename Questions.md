#Questions for people

*Q: Should I put things like stats, skills in a dictionary or simply leave them all as individual variables.
A: JP recommends that I keep them in seperte functions, to make the code mroe readable.
*Q: where do i put things like Random and CSV readers and such
A: JP recommended putting them in constructors because tey get dropped after.
However the CSV reader is too much processing for a constructor, instead create it at the begining and store it in memory