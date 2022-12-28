using System;

namespace AdmDentalOffice
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
/*
Inclusão de pacientes no cadastro: são necessários CPF, nome e data de nascimento.
a. CPF deve ser válido (vide Anexo A da lista de exercícios 2 do Aquecimento).                                              ok
b. O nome do usuário deve ter pelo menos 5 caracteres.                                                                      ok
c. A data de nascimento deve ser fornecida no formato DD/MM/AAAA.                                                           
d. Caso algum dado seja inválido, deve ser apresentada uma mensagem de erro e o dado
deve ser solicitado novamente.
e. Não podem existir dois pacientes com o mesmo CPF.                                                                        ok
f. O dentista não atende crianças, logo o paciente deve ter 13 anos ou mais no momento do
cadastro (data atual).                                                                                                      ok
2. Exclusão de pacientes do cadastro: é necessário fornecer o CPF.                                                          ok
a. Um paciente com uma consulta agendada futura não pode ser excluído.                                                      ok
b. Se o paciente tiver uma ou mais consultas agendadas passadas, ele pode ser excluído.                                     
Nesse caso, os respectivos agendamentos também devem ser excluídos.                                                         ok
3. Agendamento de uma consulta: são necessários CPF do paciente, data da consulta, hora
inicial e hora final.                                                                                                       ok
a. CPF deve existir no cadastro.                                                                                            ok
b. A data da consulta deve ser fornecida no formato DD/MM/AAAA.
c. Hora inicial e final devem ser fornecidos no formato HHMM (padrão brasileiro).
d. O agendamento deve ser para um período futuro: data da consulta > data atual ou (data da
consulta = data atual e hora inicial > hora atual).                                                                         ok
e. Hora final > hora inicial.                                                                                               ok
f. Cada paciente só pode realizar um agendamento futuro por vez (os agendamentos
passados não podem ser usados nessa verificação).                                                                           ok
g. Não pode haver agendamentos sobrepostos.                                                                                 ok

1

Formação Back-end Desafio #1
h. As horas inicial e final são definidas sempre de 15 em 15 minutos. Assim, são válidas
horas como 1400, 1730, 1615, 1000 e 0715. Por outro lado, não são válidas horas como
1820, 1235, 0810 e 1950.                                                                                                    ok
i. O horário de funcionamento do consultório é das 8:00h às 19:00h, logo os horários de
agendamento não podem sair desses limites.                                                                                  ok
4. Cancelamento de um agendamento: são necessários CPF do paciente, data da consulta e
hora inicial.
a. O cancelamento só pode ser realizado se for de um agendamento futuro (data do
agendamento > data atual ou (data do agendamento = data atual e hora inicial > hora
atual)).
5. Listagem dos Pacientes
a. A listagem de pacientes deve ser apresentada conforme o layout definido no final desse
documento e pode estar ordenada de forma crescente por CPF ou nome, à escolha do
usuário.                                                                                                                    falta fazer layout
b. Se o paciente possuir um agendamento futuro, os dados do agendamento devem ser
apresentados abaixo dos dados do paciente.                                                                                  ok (a função ja tem os dados)
6. Listagem da Agenda
a. A listagem da agenda deve ser apresentada conforme o layout definido no final desse
documento e deve estar ordenada de forma crescente por data e hora inicial.
b. O usuário pode listar toda a agenda ou a agenda de um período. Nesse último caso, deve
ser solicitada a data inicial e final (formato DD/MM/AAAA).
Regras
● Todas as datas e horas fornecidas pelo usuário devem ser válidas. Caso não sejam, deve
ser apresentada uma mensagem de erro e o dado deve ser solicitado novamente.
● Nas listagens, os dados devem estar formatados e alinhados conforme os layouts
definidos no final deste documento. 

 */