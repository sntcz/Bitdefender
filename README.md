# Bitdefender cmdlet for PowerShell
Bitdefender Control Center APIs allow developers 
to automate business workflows. The APIs are exposed 
using JSON-RPC 2.0 protocol.

This cmdlet allow PowerShell users to automate 
business workflow via PowerShell scripts.

## General parameters

### -ServerUrl
The base URL for all APIs is the machine hostname, 
domain or IP where GravityZone is installed. 
Default value is `https://cloud.gravityzone.bitdefender.com`

### -ApiKey
The API key is a unique key that is generated in MyAccount 
section of Bitdefender Control Center.


## List output general parameters

### -IncludeTotalCount
Reports the number of objects in the data set (an integer) 
followed by the objects. If the cmdlet cannot determine the total count,
it returns 'Unknown total count'.

### -Skip
Ignores the first 'n' objects and then gets the remaining objects.

### -First
Gets only the first 'n' objects.

## Get-BitdefenderApiKeyDetail
Get Bitdefender details about the API key used.

## Get-BitdefenderReportsList
Get Bitdefender list of scheduled reports, 
according to the parameters received.

## Get-BitdefenderReportDownloadLinks
Get Bitdefender information regarding the report availability 
for download and the corresponding download links.

__Note:__
To access this URL, the HTTP basic authentication header 
(username:password pair) needs to be sent, where the username 
it is your API key and the password is a an empty string. 
For more information, refer Bitdefender GravityZone API Guide.




