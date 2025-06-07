# 📘 dotnet-api-fireworks 项目文档
## 🧭 项目概览
`dotnet-api-fireworks` 是一个基于 .NET 9 和 Web API 构建的后端管理系统框架，采用模块化设计，集成了权限控制、审计日志、行为日志、全局异常处理等功能，适合作为中大型项目的起始模板。
## 📁 项目结构
项目采用分层架构，主要包含以下几个模块：
```
dotnet-api-fireworks/
├── Fireworks.Api/              # 表现层：API 控制器、路由、中间件等
├── Fireworks.Application/      # 应用层：业务逻辑、服务接口、DTOs
├── Fireworks.Domain/           # 领域层：实体模型、枚举、接口定义
├── Fireworks.Infrastructure/   # 基础设施层：数据库上下文、仓储实现、第三方服务集成
├── Fireworks.Shared/           # 共享层：共享服务
├── dotnet-api-fireworks.sln    # 解决方案文件
```
## 🔧 技术栈
- 框架：.NET 8、ASP.NET Core Web API
- 架构模式：CQRS、分层架构
- 数据库访问：Entity Framework Core
- 日志记录：Serilog
- 权限控制：基于角色的访问控制（RBAC）
- 验证：FluentValidation
- API 文档：Swagger（Swashbuckle）

## 🚀 快速开始
1. 克隆项目
```
git clone https://github.com/lhfzys/dotnet-api-fireworks.git
```
2. 配置数据库连接
在 `appsettings.json` 中设置你的数据库连接字符串：
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FireworksDb;User Id=your_user;Password=your_password;"
}
```
3. 应用数据库迁移
使用 Entity Framework Core 进行数据库迁移：
```
dotnet ef database update --project Fireworks.Infrastructure
```
4. 运行项目
在项目根目录下运行：
```
dotnet run --project Fireworks.Api
```
## 🔐 权限控制
项目实现了基于角色的访问控制（RBAC），通过自定义的授权策略和中间件进行权限验证。
- 角色管理：定义不同的角色及其权限。
- 权限分配：将权限分配给角色。
- 用户角色关联：将用户与角色关联，从而赋予用户相应的权限。
权限验证在中间件中进行，确保每个请求都经过权限检查。
## 📝 审计与行为日志
系统集成了审计日志和行为日志功能：
- 审计日志：记录用户的操作行为，包括请求路径、参数、响应结果等。
- 行为日志：通过 MediatR 的管道行为（Pipeline Behavior）记录命令和查询的执行情况。
日志信息存储在数据库中，便于后续查询和分析。
## ⚙️ 中间件
项目自定义了以下中间件：
- 异常处理中间件：统一处理未捕获的异常，返回标准的错误响应。
- 请求日志中间件：记录每个 HTTP 请求的信息，包括路径、方法、状态码等。
## 📄 API 文档
集成了 Swagger 生成 API 文档，访问地址：
```
http://localhost:5112/api-docs/index.html
```
通过 Swagger UI，可以方便地查看和测试 API 接口。
## ✅ 后续计划
- 缓存机制：集成 Redis，实现数据缓存，提高系统性能。

