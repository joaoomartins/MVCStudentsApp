pipeline {
    agent {
        dockerfile true
    }

    stages('Test'){
        echo 'Testa do testa!'
    }

    stage('Test2'){
        sh 'ls -la'
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
