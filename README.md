ascii-hex-url-decoder
=====================

This tool decodes Ascii Hex encoded data found in URLs used in recent SQL Injection attacks.

Background: 
Recently one of our websites made it to the "hot list" of the resurgent ASPRox botnet virus that attempts to do SQL inject a nasty reference to a script that creates bots out of anybody visiting your site. 

The interesting scheme behind this is that the SQL injection itself is masked behind a Ascii/Binary encoded command that gets passed through the URL. I'm no security expert, but I wager that if applications are simply black listing certain SQL commands, this attack goes through because it issues a CAST statement that decodes and then executes the malicious code on the SQL server.

In order to discover this, I had to issue my own CAST statement to decode and view the code attempting to be executed. While it was harmless to do this, it still made me a little wary about decoding this on a live SQL Server in the event something was triggered. In the event that future attacks start using this entry approach, I wanted a tool that I could use to decode the code without being on the server.

So here's the app. It is nothing fancy. You can paste your full URL from your log that has the statement into it, or just the snippet where the cast is occurring. Make sure to add the CAST(0x... to to the input, since this is a keyword used to help locate the encoded data.

Example: Enter CAST(0x48454C4C4F20574F524C44 AS VARCHAR) or http://www.foobar.net/ViewItem.aspx?id=CAST(0x48454C4C4F20574F524C44 AS VARCHAR) and press the decode button (don't worry, you will get "HELLO WORLD" as the result).

I hope this helps. Thanks to the stack overflow folks (http://stackoverflow.com) as well for helping me finish off a couple small pieces to the puzzle.
