#!/bin/bash

echo "🚀 Iniciando ambiente de desenvolvimento..."

# Verificar se o emulador já está rodando
if ! adb devices | grep -q "emulator"; then
    echo "📱 Iniciando emulador Android..."
    emulator -avd Pixel_API_34 &
    
    echo "⏳ Aguardando emulador inicializar..."
    adb wait-for-device
    sleep 10
else
    echo "📱 Emulador já está rodando"
fi

# Iniciar backend em background
echo "🔧 Iniciando backend API..."
dotnet run --project SistemaChamados.csproj &
BACKEND_PID=$!

# Aguardar backend inicializar
sleep 5

# Testar se API está respondendo
if curl -s http://localhost:5123/api/usuarios > /dev/null; then
    echo "✅ Backend API está rodando"
else
    echo "❌ Erro: Backend API não está respondendo"
    kill $BACKEND_PID
    exit 1
fi

# Compilar e executar mobile
echo "📱 Compilando e executando app mobile..."
cd Mobile
dotnet build -f net8.0-android -t:Run

echo "🎉 Ambiente iniciado com sucesso!"
echo "Backend: http://localhost:5123"
echo "Para parar o backend: kill $BACKEND_PID"