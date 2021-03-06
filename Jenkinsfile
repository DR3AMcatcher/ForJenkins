properties([
    parameters([
        string (name: 'branchName', defaultValue: 'master', description: 'Branch to get the tests from')
    ])
])

def isFailed = false
def branch = params.branchName
def buildArtifactsFolder = "C:/BuildPackagesFromPipeline/$BUILD_ID"
currentBuild.description = "Branch: $branch"

def RunNUnitTests(String pathToDll, String condition, String reportName)
{
    try
    {
        bat "C:/Program Files (x86)/NUnit.org/nunit-console/nunit3-console.exe $pathToDll $condition --result=$reportNamee"
    }
    finally
    {
        stash name: reportName, includes: reportName
    }
}

node('master') 
{
    stage('Checkout')
    {
        git branch: branch, url: 'https://github.com/DR3AMcatcher/ForJenkins.git'
    }
    
    stage('Restore NuGet')
    {
        powershell ".\\build.ps1 RestoreNuGetPackages"
    }

    stage('Build Solution')
    {
        bat '"C:/Program Files (x86)/Microsoft Visual Studio/2017/Community/MSBuild/15.0/Bin/MSBuild.exe" src/HomeWork7.sln'
    }

    stage('Copy Artifacts')
    {
        powershell ".\\build.ps1 CopyArtifacts -BuildArtifactsFolder $buildArtifactsFolder"
    }
}

catchError
{
    isFailed = true
    stage('Run Tests')
    {
        parallel FirstTest: {
            node('master') {
                RunNUnitTests("$buildArtifactsFolder/PhpTravels.UITests.dll", "--where cat==1", "TestResult1.xml")
            }
        }, SecondTest: {
            node('Slave') {
                RunNUnitTests("$buildArtifactsFolder/PhpTravels.UITests.dll", "--where cat==2", "TestResult2.xml")
            }
        }
    }
    isFailed = false
}

node('master')
{
    stage('Reporting')
    {
        unstash "TestResult1.xml"
        unstash "TestResult2.xml"

        archiveArtifacts '*.xml'
        nunit testResultsPattern: 'TestResult1.xml, TestResult2.xml'

        if(isFailed)
        {
            slackSend color: 'danger', message: 'Tests failed.'
        }
        else
        {
            slackSend color: 'good', message: 'Tests passed.'
        }
    }
}