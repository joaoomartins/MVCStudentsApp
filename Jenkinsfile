pipeline {
    agent any

    environment {
        DOCKER_COMPOSE_SERVICE = 'mvcstudentapp'
    }

    triggers {
        // Configurar o gatilho para acionar a pipeline no push para o repositório
        pollSCM('* * * * *')
    }

    stages {
        stage('Checkout') {
            steps {
                script {
                    // Checkout do repositório
                    checkout scm
                }
            }
        }

        stage('Build and Run Docker Compose') {
            steps {
                script {
                    // Construir e executar o Docker Compose
                    sh "docker-compose up --build -d $DOCKER_COMPOSE_SERVICE"
                }
            }
        }
    }

    post {
        success {
            echo 'Pipeline executada com sucesso!'
        }
        failure {
            echo 'A pipeline falhou. Verifique os logs para mais detalhes.'
        }
    }
}
