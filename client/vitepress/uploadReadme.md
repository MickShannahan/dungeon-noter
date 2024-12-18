# 🌩️ Cloud Function File Uploading

## A little about the technology
While it's simple to add a URL for an image to any model of data, that's not typically how web apps work. If you're curious about whether we knew that, we did, but file upload itself isn't the quickest thing to implement and can add a lot of dev time to our applications. We need a file storage somewhere else than our document or table databases, we need to configure access to it so our client can upload to it, and typically some way to compress the data the user is trying to upload so we can minimize our costs.

Instead of going through this process for each and every app we build, it's more efficient to create a serverless function that can handle this style of upload for us that can be used from any of our apps.


### Enter Cloud Functions

Cloud functions are chunks of code stored in the cloud. You trigger it, and it runs. Often these functions are triggered by events like HTTP requests, much like an API. The main benefit of using cloud functions is that we get to hand off all of the infrastructure to our cloud provider, so all we have to do is write the code we want our function to run, and that's it.


### Blob Storage
What is blob storage? 
[A little about blobs](https://learn.microsoft.com/en-us/rest/api/storageservices/understanding-block-blobs--append-blobs--and-page-blobs?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json#about-block-blobs)



## 🔧 Setup and preparation

You will need to complete these before you can begin.

### Azure Cloud Account
> ⚠️*While there are many free things you can do with Azure, and Azure offers credits for new accounts, this will not be free. Depending on your use case though It can be very very cheap (My personal use costs me less than $1 a month)*

You will need to [create an Azure Cloud Account](https://azure.microsoft.com/en-us/free/). If you want to sign up using GitHub that is recommended. You will also need to make sure you have a [payment method set up](https://learn.microsoft.com/en-us/azure/cost-management-billing/manage/change-credit-card).

### Create an Azure Subscription 
[🔗Azure Docs](https://learn.microsoft.com/en-us/azure/cost-management-billing/manage/create-subscription#create-a-subscription)
Azure subscriptions are how you track usage costs, while just having one subscription is nice and keeps things simple you may want to consider multiple subscriptions per client.

![Azure Subscription](assets/SubscriptionsLogo.png)
![Create Subscription](assets/AzureCreateSub.png)
![Create Subscription](assets/AzureCreateBudget.png)


### Create an Azure Storage Account

![Azure Storage](assets/StorageLogo.png)
![Create Storage Account](assets/StorageAccountBasics.png)
![Create Storage Security](assets/StorageAccountSecurity.png)


### Create a Container in your storage Account

![Create Container](assets//ContainerCreate.png)


### Create an Azure Cloud Function App

![Azure Storage](assets/FunctionAppLogo.png)
![Create Function App](assets/AzureCreateFunction.png)

> ⚠️ *you will need to use the Windows OS if you want to run the deploy from the Azure extension. There are other ways to deploy for a Linux OS that aren't hard, but definitely not as easy as a click of a button*


### Download the Azure Tools Extension

  Time to head over to vscode. You might want to create a folder for this project and open vscode there while you are at it.

  Once open download and Log into the Azure tools extension

  ![Download Azure Tools](assets/AzureToolsExtension.png)


## 💻 The Code
 
### Create an Empty folder 
open this folder in vs code

### Create Function from Azure tools template

`ctrl+shift+p` => Azure Create Function => HTTP trigger => name it (image-upload) => This will create all the necessary files for the function to work.

### Setup the local.settings.json

You will want to add your auth0 keys to the `values` section of the JSON file and fill them out. the `AzureWebJobsStorage` also needs to be filled in. You can get this connection string from the Azure extension by right-clicking on the storage account.

![Copy Connection](assets/SettingsConnectionString.png)

```json
"AUTH0_DOMAIN": "",
"AUTH0_AUDIENCE": "",
"AUTH0_CLIENT_ID": ""
```

### Spin up and test

You can either use `f5` or the debug menu to start a local instance of your function, you should get a success message telling you the address you can reach the function from 

![Terminal Message](assets/TerminalSpinUpMessage.png)

If you head over to Postman, you should be able to send a `POST` or `GET` Request to the endpoint listed in your console and get a response back!

![Terminal Message](assets/PostManTest1.png)

You have your first cloud function!

One last thing we are going to do before we move on is update the code here to use, modules, that way use import statements like we are used to instead of requiring statements like in the template.
  - Rename your function so that it ends with a `.mjs`
  - Update the package.json to include `.mjs`

  ![Package update](assets/PackagUpdate.png)

  - update our code to `import` app instead of require

  ![ReqToImport](assets/RequireToImport.png)

  - 🗝️ *make sure to stop and start this function and test it again in Postman.*


### Almost ready to upload

Before we move onto uploading we want to install some node modules to this function.
Jump into your console and import the following packages

#### install node packages

  - `@azure/storage-blob` lets us create and upload "blobs" to our storage account
  -  `@bcwdev/auth0provider` Allows us to implement user authentication
  -  `parse-multipart` Offers a quick solution to pulling the file data from our request
  -  `sharp` Not necessary, but offers a great amount of tools for image processing 


### 🔒 Authorizing Requests

We want to make sure that people making requests to this endpoint are authorized. This ensures that they have logged into one of our applications first. For now, we just want to spin up any of our applications and get a token from them for the postman to use. We first will want to build out a try-catch so any thrown errors are returned to the user. 

![Try Catch](assets/CodeTryCatch.png)

Jump into the example code to the `⚓AUTHORIZE` anchor, and build out what you see.


Make sure to stop and test this. You can use a `context` to log the userInfo into your console and check if the userInfo is being pulled out.

![User Context Log](assets/UserResponse.png)

### 📡 Uploading Files

Let's Upload a file to our function using Postman. Set the method to `POST`, and in the body options for the request, select form-data. For the key select `file`. This gives us a drop-down and lets us select a file from our computer to send.

![Postman File Upload](assets/PostmanFileUpload.png)


We will be using this picture of an orangutan... for no reason in particular.
![Orangutan](assets/orangutan.png)

Like before jump to the `⚓GET FILE`, and build along with the comments. You should be able to stop and once again context log the details of the file you chose to upload.

![Console log of file](assets/FIleLog.png)

### ☁️ Save to Blob

Now that we have the file in our function, let's connect to our blob storage container, and save the file to it. Go to the `⚓SAVE IMAGE` and follow along.

This time to test we are going to head back into the Azure Portal, where we created our container earlier, and see if the file was uploaded. If you're not sure how to get there try
> storage accounts => `your storage` => containers => `your container` where you should see a folder labeled with the `id` of the user logged in, and the image inside


![Result of Uploaded File](assets/UploadedFile.png)

click the blue `copy` button to copy the URL, and drop it into the URL of a new tab. You should see an image you uploaded appear.

> ⚠️ A common error will be the "resource not found" error. This occurs when permissions to the storage OR container are not set for anonyms reading.

Here are where those settings are for both.

Container; should be set to `blob`
![Container Access location](assets/ContainerAccess.png)

Account; Allow blob anonymous access Enabled
![Account Access location](assets/AccountAccess.png)

### 🛰️ Respond!

The last step is to respond with the relevant information. In this case, we give them back the URL to access the image for any of their needs, the size in kb, and the file name. They will need to save this file name if they want to delete this file later.

## 📦 Deploy

Now we are ready to deploy our function app and use our function from anywhere.

First thing first, if we have not initialized a git repo and committed, we should do that now!

Azure's extension makes the deployment super easy. Like just a few mouse clicks from VScode is easy. Head over to the extension and expand out your function app. It will be under a could drop downs for Subscriptions and resource groups.

Right-click on the function app ⚡and set the deployment source as the local git. This connects your git repo to the function app in Azure

![Configure Git](assets/AzureConfigure.png)

Expand the function app and right-click on the `Application Settings`, then `Upload Local Settings`. This takes the local.settings.json and uploads its values to Azure for our app to use.

Right-click on the function app ⚡ again, then `Deploy to function app`. That's it. The function app is ready to use. You can head over to your function app on the Azure portal to see your function.

![Azure Live Functions](assets/FunctionsLiveOnAzure.png)

> 💬 It is super easy to deploy but a small note, I have had mixed results with this deployment being successful. I have clicked it once and had it work perfectly, I have done it 5 times, and failed 4 out of the 5. There is an output log that will tell you if there are issues with the project but seems like sometimes it just fails. You might just want to click deploy again if it doesn't work.


## 🚮 Deleting

Well, there are a few things to do more, one is to make sure users can delete images from blob storage.  This is more or less the exact same process from before but without all the setup steps. Take a look at the function to delete an image and you will notice many similarities.

We don't need to worry about auth in this case since everything is organized into folders with the user's `id`.

## 📸 Improvements with Sharp

Sharp is a library focused on image processing, it makes many common image processing tasks like re-sizing and compressing very easy.

We rarely need or want to save the full-resolution image a user sends to us. Consider a profile picture taken from someone's phone. These are usually very high resolution, and are going to be displayed in little bubbles in our app, fractions of the original image size.

This project has an extra function `sharpUpload` that includes a couple steps taken to process the images uploaded.

#### webp

Webp is a modern format designed for the web. If at all possible you should be using webp whenever you can. It is flexible enough for lossy and lossless compression, oftentimes cutting image sizes in half.

[Sharp Docs](https://sharp.pixelplumbing.com/)