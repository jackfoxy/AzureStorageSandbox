namespace AzureStorageSandbox

//open FSharp.Configuration
//open FSharp.Azure.StorageTypeProvider
//open Microsoft.WindowsAzure.Storage.Queue
//open Microsoft.WindowsAzure.Storage.RetryPolicies

type AppSettings = FSharp.Configuration.AppSettings<"App.config">
open FSharp.Azure.StorageTypeProvider

open System.Configuration
open Microsoft.WindowsAzure.Storage

type Local = AzureTypeProvider<"UseDevelopmentStorage=true">

module Common =
    //let connString = ConfigurationManager.AppSettings.["tibrutest12_STORAGE"]
    let connString = "UseDevelopmentStorage=true"
    let storageAccount = CloudStorageAccount.Parse(connString)



