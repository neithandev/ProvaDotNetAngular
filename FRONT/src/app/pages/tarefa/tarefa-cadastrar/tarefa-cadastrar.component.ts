import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Tarefa } from "src/app/models/tarefa.model";


@Component({
    selector: 'app-tarefa-cadastrar',
    templateUrl: './tarefa-cadastrar.component.html',
    styleUrls: ['./tarefa-cadastrar.component.css']
  })

export class TarefaCadastrarComponent {
  titulo: string = "";
  descricao: string = "";
  
  constructor(private client: HttpClient, private router: Router) {}

    cadastrar(): void {
        let tarefa: Tarefa = {
        titulo: this.titulo,
        descricao: this.descricao,
        tarefaId: 0,
        categoriaId: 0,
        statusId: 0
        };
    
        this.client.post<Tarefa>("https://localhost:7015/api/tarefa/cadastrar", tarefa).subscribe({
          next: (produto) => {
            this.router.navigate(["pages/tarefa/listar"]);
          },
          error: (erro) => {
            console.log(erro);
          },
        });
        }
    }