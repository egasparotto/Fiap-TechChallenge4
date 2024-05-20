### FIAP RESERVAS
Projeto criado para o Tech Challenge da fase 4 do curso de pós graduação em Arquitetura de Sistemas .NET com Azure.

Turma 2023/2 GRUPO 12

Link do GitHub: [https://github.com/egasparotto/Fiap-TechChallenge4](https://github.com/egasparotto/Fiap-TechChallenge4)

#### Descrição

O projeto FiapReservas é uma aplicação web API desenvolvida em .NET Core com C#. O objetivo principal é fornecer uma plataforma para gerenciar reservas em restaurantes. A aplicação segue os princípios da Arquitetura Limpa, priorizando a separação de preocupações, independência de UI, regras de negócio e infraestrutura.

#### Levantamento de Requisitos e Critérios de Aceite

O levantamento de requisitos e critérios de aceite foi documentado detalhadamente no README do repositório do GitHub. Este documento serviu como guia para o desenvolvimento do projeto, garantindo uma compreensão clara dos objetivos e funcionalidades esperadas. Você pode encontrar o documento de requisitos abaixo.

---

# Levantamento de Requisitos e Critérios de Aceite

## Introdução
O projeto FiapReservas é uma aplicação web API desenvolvida para gerenciar reservas em restaurantes. Este documento detalha os requisitos do sistema, incluindo funcionalidades esperadas, restrições técnicas e critérios de aceite para cada requisito.

## Requisitos Funcionais
1. **Cadastro de Usuário**
   - O sistema deve permitir o cadastro de novos usuários.
   - Cada usuário deve fornecer um nome, e-mail e senha.
   - O sistema deve criptografar a senha antes de armazená-la no banco de dados.
   - Critérios de Aceite:
     - O usuário deve fornecer todos os campos obrigatórios.
     - O e-mail fornecido deve ser único no sistema.
     - A senha deve ter pelo menos 8 caracteres.

2. **Autenticação de Usuário**
   - O sistema deve permitir que usuários autenticados acessem determinadas funcionalidades.
   - Usuários autenticados devem receber um token JWT para autorização.
   - Critérios de Aceite:
     - O sistema deve validar o e-mail e a senha fornecidos pelo usuário.

3. **Gerenciamento de Restaurantes**
   - O sistema deve permitir o cadastro, atualização, exclusão e listagem de restaurantes.
   - Cada restaurante deve possuir um nome, descrição e número de telefone.
   - Critérios de Aceite:
     - Todas as operações CRUD devem ser realizadas com sucesso no banco de dados.

4. **Gerenciamento de Reservas**
   - O sistema deve permitir a reserva de mesas em restaurantes.
   - Os usuários devem fornecer informações como nome, e-mail, telefone, data da reserva, restaurante e quantidade de pessoas.
   - As reservas devem ter status (Solicitada, Confirmada, Cancelada).
   - Critérios de Aceite:
     - As reservas devem ser registradas com sucesso no banco de dados.
     - O sistema deve calcular a disponibilidade de mesas no restaurante para a data especificada.

## Requisitos Não Funcionais
1. **Segurança**
   - O sistema deve garantir a segurança das informações dos usuários.
   - As senhas devem ser criptografadas antes de serem armazenadas no banco de dados.
   - O acesso às funcionalidades restritas deve ser controlado por autenticação JWT.

2. **Performance**
   - O sistema deve ser responsivo e fornecer uma experiência de usuário fluente.
   - As consultas ao banco de dados devem ser otimizadas para garantir tempos de resposta rápidos.

3. **Escalabilidade**
   - O sistema deve ser capaz de lidar com um grande número de usuários e reservas simultaneamente.
   - A arquitetura da aplicação deve ser escalável, permitindo futuras expansões e atualizações.

4. **Manutenibilidade**
   - O código-fonte deve ser organizado, documentado e seguir as melhores práticas de desenvolvimento.
   - A aplicação deve ser facilmente mantida e atualizada por outros desenvolvedores.

## Critérios de Aceite Gerais
- Todos os requisitos funcionais e não funcionais devem ser implementados conforme especificado.
- As funcionalidades devem ser testadas e validadas em diferentes cenários de uso.
- O sistema deve ser entregue sem bugs críticos ou falhas de segurança.
- A documentação do projeto deve ser clara e abrangente, facilitando futuras manutenções e atualizações.

Este documento servirá como guia para o desenvolvimento do projeto FiapReservas, garantindo que todas as funcionalidades sejam implementadas de acordo com as expectativas e requisitos estabelecidos.

---

#### Desenvolvimento da Aplicação

A estrutura do projeto reflete os princípios da Arquitetura Limpa, com uma clara separação entre as camadas de domínio, aplicação, infraestrutura e apresentação/UI. A aplicação foi desenvolvida com foco em escalabilidade, manutenibilidade e facilidade de teste.

#### Persistência de Dados

Os dados da aplicação são persistidos em um banco de dados MongoDB. A escolha do banco de dados NoSQL foi justificada com base nos requisitos do projeto, incluindo a flexibilidade de esquema e a escalabilidade horizontal.

#### Testes

Foram implementados testes unitários e de integração para cobrir as principais lógicas de negócio, integração com o banco de dados e interações entre diferentes camadas de aplicação. Embora a cobertura não seja de 100%, os testes garantem a qualidade e integridade das funcionalidades críticas do sistema.

### Como Executar o Projeto

1. Clone o repositório do GitHub: `git clone https://github.com/egasparotto/Fiap-TechChallenge4.git`
2. Navegue até o diretório do projeto: `cd Fiap-TechChallenge4`
3. Execute o seguinte comando: `docker compose up -d --no-deps --build`
4. Após a inicialização dos containers, é possível acessar o Swagger pela seguinte URL: [http://localhost:9999/swagger/index.html](http://localhost:9999/swagger/index.html)
5. Para acessar a UI, utilize o seguinte link: [http://localhost:8085/home](http://localhost:8085/home)

#### Apresentação do Projeto

Confira a apresentação do projeto no vídeo abaixo:

[Link do Vídeo no YouTube](https://youtu.be/yIyOCnz9Nbs)
