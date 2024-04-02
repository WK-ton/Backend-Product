pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/WK-ton/Backend-Product'
            }
        }
        // stage('logout Docker'){
        //     steps {
        //         script{
        //              sh 'docker logout'
        //         }
        //     }
        // }
        stage('Build Docker images') {
            steps {
                script {
                    sh 'sudo docker build -t tontwitch04/api:v1.0 .'
                }
            }
        }
        stage('Push Image to Hub'){
            steps {
                script {
                    withCredentials([string(credentialsId: 'dockerhubpwd', variable: 'dockerhubpwd')]) {
                    sh 'sudo docker login -u tontwitch04@gmail.com -p ${dockerhubpwd}'
                }
                    sh 'sudo docker push tontwitch04/api:v1.0'
                }
            }
        }
        
    }
}
