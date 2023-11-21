pipeline {
    agent {
        dockerfile true
    }

    stages {
        stage('Clean workspace') {
            steps {
                cleanWs()
            }
        }

        stage('Git Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/joaoomartins/MVCStudentsApp.git' 
            }
        }

        stage('Restore packages') {
            steps {
                bat "dotnet restore ${workspace}\\MVCStudentsApp\\MVCStudentsApp\\MVCStudentsApp.csproj"
            }
        }

        stage('Clean') {
            steps {
                bat "msbuild.exe ${workspace}\\MVCStudentsApp\\MVCStudentsApp.sln /nologo /nr:false /p:platform=\"x64\" /p:configuration=\"release\" /t:clean"
            }
        }
        
        stage('Increase version') {
            steps {
                powershell '''
                    $xmlFileName = "${workspace}\\MVCStudentsApp\\MVCStudentsApp\\Package.appxmanifest"
                    [xml]$xmlDoc = Get-Content $xmlFileName
                    $version = $xmlDoc.Package.Identity.Version
                    $trimmedVersion = $version -replace '.[0-9]+$', '.'
                    $xmlDoc.Package.Identity.Version = $trimmedVersion + ${env.BUILD_NUMBER}
                    echo 'New version:' $xmlDoc.Package.Identity.Version
                    $xmlDoc.Save($xmlFileName)
                '''
            }
        }

        stage('Build') {
            steps {
                bat "msbuild.exe ${workspace}\\MVCStudentsApp\\MVCStudentsApp.sln /nologo /nr:false /p:platform=\"x64\" /p:configuration=\"release\" /p:PackageCertificateKeyFile=<path-to-certificate-file>.pfx /t:clean;restore;rebuild"
            }
        }

        stage('Publish HTML report') {
            steps {
                publishHTML(target: [allowMissing: false, alwaysLinkToLastBuild: false, keepAll: false, reportDir: 'CodeCoverage_${BUILD_NUMBER}', reportFiles: 'index.html', reportName: 'HTML Report', reportTitles: 'Code Coverage Report'])
            }
        }
    } 
}
