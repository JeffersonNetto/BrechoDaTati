export class Pedido {
    Id!: string
    ClienteId!: string
    DataCriacao!: Date
    CupomId: number | undefined
    StatusId!: number
}