export interface Payment {
    id: string;
    assignmentId: string;
    paymentDate: Date;
    payer: string;
    payee: string;
    paymentAmount: number;
}
