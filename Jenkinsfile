pipeline {
    agent {
        dockerfile true
    }

    environment {
        DOCKER_COMPOSE_SERVICE = 'mvcstudentapp'
    }

    triggers {
        pollSCM('* * * * *')
    }

    stages {
        stage('Checkout') {
            steps {
                script {
                    checkout scm
                }
            }
        }

        stage('Build and Run Docker Compose') {
            steps {
                script {
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
