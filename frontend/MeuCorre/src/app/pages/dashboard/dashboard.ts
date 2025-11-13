import { Component } from '@angular/core';
import { AcessoRapido } from "./acesso-rapido/acesso-rapido";
import { BemVindo } from './bem-vindo/bem-vindo';

@Component({
  selector: 'app-dashboard',
  imports: [AcessoRapido, BemVindo],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {

}
