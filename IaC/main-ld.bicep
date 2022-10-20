param webAppName string = uniqueString(resourceGroup().id) // Generate unique String for web app name
param sku string = 'S1' // The SKU of App Service Plan
param location string = resourceGroup().location // Location for all resources

param linuxFxVersion string = 'DOTNETCORE|6.0' // The runtime stack of web app
param suffix string = '-cayers${uniqueString(resourceGroup().id)}'

var appServicePlanName = toLower('AppServicePlan-FeatureFlags')
var webSiteName = toLower('wapp-${webAppName}${suffix}')


resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: sku
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: webSiteName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion

    }
  }
}

output webAppName string = appService.name
