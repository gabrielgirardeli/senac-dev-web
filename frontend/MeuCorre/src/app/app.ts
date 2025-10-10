import { Component, computed, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RouterLink } from "../../node_modules/@angular/router/router_module.d";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('MeuCorre - Finanças Pessoais');


  nome = signal("Gabriel Silva");
  alternarNome(apcao: number) {
    if (apcao === 1) {
      this.nome.set("Gabriel")
    } else {
      this.nome.set("Gabriel Silva")
    }
    
}
 total300 = computed(() => this.total300() * 20);



}
