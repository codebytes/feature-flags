param webAppName string = uniqueString(resourceGroup().id) // Generate unique String for web app name
param sku string = 'S1' // The SKU of App Service Plan
param location string = resourceGroup().location // Location for all resources

param linuxFxVersion string = 'DOTNETCORE|5.0' // The runtime stack of web app

var appServicePlanName = toLower('AppServicePlan-FeatureFlags')
var webSiteName = toLower('wapp-${webAppName}')

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

module appConfig 'appconfig.bicep' = {
  name: 'appconfig'
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

resource connectionString 'Microsoft.Web/sites/config@2020-06-01' = {
  parent: appService
  name: 'connectionstrings'
  properties: {
    'AppConfig': {
      value: appConfig.outputs.configStoreConnectionString
      type:'Custom'
    }
  }
}

output webAppName string = appService.name
