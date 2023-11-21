pipeline {
    
    agent {
        dockerfile true
    } 

    stages {
        stage('Test') {
            steps {
                echo 'Testa do Teste!'
            }
        }

        stage('Test2') {
            steps {
                sh 'ls -la'
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
