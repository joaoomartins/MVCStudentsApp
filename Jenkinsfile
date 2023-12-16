pipeline {
    agent any 

    stages {
        stage('Test') {
            steps {
                echo 'Testa do Teste!'
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
