import { PedidoItem } from "./PedidoItem"

export class Pedido {
    Id!: string
    ClienteId!: string
    DataCriacao!: Date
    Ip: string
    CupomId: number | undefined
    StatusId!: number
    PedidoItem: PedidoItem[] | undefined
}