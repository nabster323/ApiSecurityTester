[![Build Status](https://travis-ci.org/nabster323/ApiSecurityTester.svg?branch=master)](https://travis-ci.org/nabster323/ApiSecurityTester)

# ApiSecurityTester
A tool that provides quick feedback on an API and its security level. Eventually I will enable it to be run as part of a CI pipeline.

## Do you use BDD / Gherkin?

I wanted to avoid things like Gherkin. I think people already use similar higher up (given, when, then) within scrum methodologies.

Which, past that it's down to interpretation anyway. So I want to make that an abstract part of this project and simply accept that you'll tell the tool with JSON For /api/GetUsers/ I expect a 401 when I'm not authenticated. E.g. trust your software engineers, automation engineers, security testers etc :-)

## Why? Aren't there other tools?

Yes and no. I wanted a really simple "You're not being an idiot" tool so that I can be reassured that I've not missed an authorize attribute or similar.

It is so easy to develop a new asp.net core API and forget to enable authentication (maybe not for the whole project, but for a new api controller or one location)

At the point of creation, this tool is not intended to be a transparent/interception proxy of any kind and it will simply be given an end point, a few things to lookout for and off it goes!

Other tools that I recommend you look at (because they are brilliant):
1. Owasp ZAP - https://www.owasp.org/index.php/OWASP_Zed_Attack_Proxy_Project
1. Fiddler - https://www.telerik.com/fiddler
1. BDD security - https://continuumsecurity.net/category/guides/ (overly complex, hence my reason for this tool)

## Getting started

As this is so new, I don't know what on eath to put here yet. However, I will make sure to have a release folder where you can simply retrieve the latest build and run it. You will need the .net core runtime to do so.

## Run anywhere

This tool will run (courtesy of .net core!) on Windows, Linux and Mac!

## Hints, suggestions?

Make a PR, I'll glady review any I receieve unless they're better than my work; in which case they'll be rejected immediately ;-)

## Usage

I'm predicting the future a little here as at this point I don't even have a project. However:

1. Get the latest release (or source if you prefer to build yourself)
1. Configure your test(s)
1. Make it part of your "checkin dance" and Definition of Done

## Example config file (included within project):

```
{
  "Name": "Ensure 401 given for no session",
  "BaseUri": "https://localhost:44358/api",
  "OnlyHttps": true,
  "Tests": [
    {
      "Verbs": [ "GET", "POST" ],
      "ExpectedHttpResponseCode": 401,
      "ExpectedHttpResponseContentType": "application/json",
      "EndPoint": "/Test/Return401"
    }
  ]
}
```
