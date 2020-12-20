export class Usuario {
    Email!: string
    Senha!: string
    Token: string | undefined    
    RefreshToken: string | undefined
}