<div style="text-align:center;">
  <h1>ServiceHub – Marketplace de Serviços</h1>
  <p>
   <img src="https://img.shields.io/badge/Status-Em%20Construção-orange" alt="Em Construção"/>
</p>
  <p>
    <img src="https://img.shields.io/badge/ASP.NET%20Core-9.0-blue" alt="ASP.NET Core"/>
    <img src="https://img.shields.io/badge/Entity%20Framework-Core-green" alt="Entity Framework Core"/>
    <img src="https://img.shields.io/badge/ASP.NET%20Identity-Auth-blueviolet?logo=.net" alt="ASP.NET Identity"/>
    <img src="https://img.shields.io/badge/SQL%20Server-Database-red" alt="SQL Server"/>
    <img src="https://img.shields.io/badge/Swagger-API%20Docs-yellowgreen" alt="Swagger"/>
  </p>
</div>



<div style="text-align:center; margin-bottom:20px;">
  <a href="#sobre-o-projeto">Sobre</a> |
  <a href="#funcionalidades">Funcionalidades</a> |
  <a href="#tecnologias">Tecnologias</a> |
  <a href="#como-executar">Como Executar</a> |
  <a href="#documentacao">Documentação</a>
</div>

<hr/>

<h2 id="sobre-o-projeto">📌 Sobre o Projeto</h2>

<p>
  O <strong>ServiceHub</strong> é uma plataforma online que conecta pessoas que precisam de serviços com profissionais que oferecem esses serviços de forma prática, rápida e segura.
</p>

<h3>🎯 Objetivo do Projeto</h3>
<p>
  Este projeto foi desenvolvido para colocar em prática conceitos e tecnologias estudadas, aplicando conhecimentos teóricos em um projeto funcional, escalável e seguro.
</p>

<h4>Principais conceitos aplicados:</h4>
<ul>
  <li><strong>Arquitetura Limpa (Clean Architecture)</strong> – Separação de camadas e desacoplamento.</li>
  <li><strong>Padrão CQRS</strong> – Separação entre Commands e Queries para melhor organização e performance.</li>
  <li><strong>ASP.NET Core</strong> – Criação de uma API robusta e escalável.</li>
  <li><strong>Banco de Dados Relacional (SQL Server)</strong> – Persistência de dados estruturada.</li>
  <li><strong>ASP.NET Identity</strong> – Gerenciamento completo de autenticação e usuários.</li>
  <li><strong>Autenticação e Autorização (JWT)</strong> – Segurança da API com tokens.</li>
  <li><strong>Testes Unitários (xUnit)</strong> – Garantia de qualidade e estabilidade do código.</li>
  <li><strong>Pacotes e Ferramentas</strong> – Entity Framework Core, FluentValidation, JwtBearer, entre outros.</li>
</ul>

<hr/>

<h2 id="funcionalidades">⚙ Funcionalidades do Sistema</h2>

<h3>Gerenciamento de Usuários</h3>
<ul>
  <li>Registro e autenticação de usuários.</li>
  <li>Diferenciação de perfis (administradores, prestadores e clientes).</li>
  <li>Recuperação de senha e atualização de dados.</li>
</ul>


<h3>Segurança</h3>
<ul>
  <li>Autenticação baseada em JWT.</li>
  <li>Controle de acesso baseado em roles e permissões.</li>
</ul>

<hr/>

<h2 id="tecnologias">🛠 Tecnologias Utilizadas</h2>
<ul>
  <li><strong>ASP.NET Core 9.0</strong> – Framework web e API.</li>
  <li><strong>Entity Framework Core</strong> – ORM para persistência.</li>
  <li><strong>SQL Server</strong> – Banco de dados relacional.</li>
  <li><strong>ASP.NET Identity</strong> – Autenticação e gerenciamento de usuários.</li>
  <li><strong>JWT (JSON Web Token)</strong> – Segurança baseada em tokens.</li>
  <li><strong>Swagger</strong> – Documentação interativa da API.</li>
  <li><strong>xUnit</strong> – Testes unitários.</li>
  <li><strong>FluentValidation</strong> – Validação de objetos e DTOs.</li>
</ul>

<hr/>

<h2 id="como-executar">🚀 Como Executar o Projeto</h2>
<ol>
  <li>Clone o repositório:
    <pre><code>git clone https://github.com/iseverton/ServiceHub.git</code></pre>
  </li>
  <li>Configure o banco de dados no <code>appsettings.json</code>.</li>
  <li>Execute as migrações:
    <pre><code>dotnet ef database update</code></pre>
  </li>
  <li>Execute a API:
    <pre><code>dotnet run</code></pre>
  </li>
  <li>Acesse a documentação da API via Swagger:
    <pre><code>https://localhost:5001/swagger</code></pre>
  </li>
</ol>

<hr/>

<h2 id="documentacao">📚 Documentação</h2>

<p align = "center">
  <a href= "#usuarios">👥 Estrutura de Usuários</a> |
  <a href= "#x">X</a>
</p>

<h2 id="usuarios">👥 Estrutura de Usuários</h2>

<p>O ServiceHub possui três tipos de usuários que representam diferentes papéis no sistema. A separação entre essas entidades segue a arquitetura do ASP.NET Identity.</p>

<h3>1. ApplicationUser</h3>
<ul>
  <li>Entidade base vinculada ao <strong>ASP.NET Identity</strong>.</li>
  <li>Armazena informações essenciais para autenticação, como email, senha e telefone.</li>
  <li>Não contém dados específicos do perfil do usuário; serve como camada de autenticação.</li>
  <li>Exemplo de campos: <code>Email</code>, <code>PasswordHash</code>, <code>PhoneNumber</code>.</li>
</ul>

<h3>2. User</h3>
<ul>
  <li>Representa usuários do tipo cliente que utilizam a plataforma para solicitar serviços.</li>
  <li>Contém informações adicionais que não estão no ApplicationUser, como endereço, histórico de pedidos, etc.</li>
  <li>Relaciona-se com <code>ApplicationUser</code> via chave estrangeira.</li>
</ul>

<h3>3. Provider</h3>
<ul>
  <li>Representa usuários do tipo prestador de serviços.</li>
  <li>Possui informações específicas como categorias de serviço, avaliação e review.</li>
  <li>Também se relaciona com <code>ApplicationUser</code> via chave estrangeira.</li>
</ul>

<h3>📌 Fluxo de Criação de Usuário</h3>
<ol>
  <li>O usuário se registra através do <strong>ApplicationUser</strong> (camada Identity).</li>
  <li>Após autenticação, o sistema cria um registro correspondente em <strong>User</strong> ou <strong>Provider</strong>, dependendo do tipo de usuário.</li>
  <li>Todos os dados sensíveis de autenticação ficam no ApplicationUser, garantindo segurança e consistência.</li>
</ol>

