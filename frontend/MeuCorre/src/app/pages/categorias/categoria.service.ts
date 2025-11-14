import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriaModel } from './models/categoria.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  private apiUrl = 'https://localhost:7160/Categoria';
  private http = inject(HttpClient);

  

  obterTodasPorUsuario(): Observable<CategoriaModel[]> 
  {
    return this.http.get<CategoriaModel[]>(`${this.apiUrl }/ObterTodasPorUsuario`); // corrigir isso gabriel 14/11/2025

  }
}
