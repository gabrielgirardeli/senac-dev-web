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
   const result = this.http.get<CategoriaModel[]>(`${this.apiUrl}?Usuarioid = da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b`);
   console.log(result);
   return result;
  }
}
