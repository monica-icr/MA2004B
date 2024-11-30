import pandas as pd
import random
import streamlit as st
import matplotlib.pyplot as plt
from io import BytesIO

def organizar_entregas(C):
    C = C.sort_values(by=' Fecha Limite').reset_index(drop=True)
    return C

def seleccion_aleatoria(C):
    C = organizar_entregas(C)
    pesos = [1 / (i + 1) for i in range(len(C))]
    Orden_utilizado = []
    indices_disponibles = list(range(len(C)))
    C_ordenado = pd.DataFrame(columns=C.columns)

    while indices_disponibles:
        pesos_disponibles = [pesos[i] for i in indices_disponibles]
        total_pesos = sum(pesos_disponibles)
        probabilidades = [p / total_pesos for p in pesos_disponibles]
        numero_seleccionado = random.choices(indices_disponibles, weights=probabilidades, k=1)[0]
        Orden_utilizado.append(numero_seleccionado)
        C_ordenado = pd.concat([C_ordenado, C.iloc[[numero_seleccionado]]])
        indices_disponibles.remove(numero_seleccionado)
    return C_ordenado

def preprocesar_rates(df2):
    rates = {}
    for _, row in df2.iterrows():
        rates[(row['IdMaquina'], row['IdProducto'])] = row['Rate']
    return rates



def acomodo(df, df2):
    maquinas_fase1 = {0: 0, 1: 0, 2: 0}
    hornos = {3: 0, 4: 0}
    maquinas_fase3 = {5: 0, 6: 0, 7: 0}

    last_product_fase1 = {0: None, 1: None, 2: None}
    last_product_fase2 = {3: None, 4: None}
    last_product_fase3 = {5: None, 6: None, 7: None}

    hora_inicio = 0
    hora_limite = 16
    dia_actual = 0
    pedidos_tardios = 0

    rates = preprocesar_rates(df2)
    rutas_pedidos = []

    dias_semana = ["Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"]

    for i, pedido in df.iterrows():
        id_producto = pedido[' IdProducto']
        cantidad = pedido['Cantidad']
        fecha_limite = pedido[' Fecha Limite']

        maquina_fase1 = min(maquinas_fase1, key=maquinas_fase1.get)
        setup_time_fase1 = 0.5 if last_product_fase1[maquina_fase1] not in (None, id_producto) else 0
        tiempo_fase1 = cantidad / rates.get((maquina_fase1, id_producto), 0) + setup_time_fase1
        inicio_fase1 = maquinas_fase1[maquina_fase1]
        fin_fase1 = inicio_fase1 + tiempo_fase1
        maquinas_fase1[maquina_fase1] += tiempo_fase1
        last_product_fase1[maquina_fase1] = id_producto

        horno = min(hornos, key=hornos.get)
        setup_time_fase2 = 1 if last_product_fase2[horno] not in (None, id_producto) else 0
        inicio_fase2 = max(hornos[horno], fin_fase1)
        tiempo_fase2 = cantidad / rates.get((horno, id_producto), 0) + setup_time_fase2
        fin_fase2 = inicio_fase2 + tiempo_fase2
        hornos[horno] = fin_fase2
        last_product_fase2[horno] = id_producto

        maquina_fase3 = min(maquinas_fase3, key=maquinas_fase3.get)
        setup_time_fase3 = 0.5 if last_product_fase3[maquina_fase3] not in (None, id_producto) else 0
        inicio_fase3 = max(maquinas_fase3[maquina_fase3], fin_fase2)
        tiempo_fase3 = cantidad / rates.get((maquina_fase3, id_producto), 0) + setup_time_fase3
        fin_fase3 = inicio_fase3 + tiempo_fase3
        maquinas_fase3[maquina_fase3] = fin_fase3
        last_product_fase3[maquina_fase3] = id_producto

        hora_entrega = fin_fase3

        if hora_entrega > hora_limite:
            dia_actual += 1
            maquinas_fase1 = {k: 0 for k in maquinas_fase1}
            hornos = {k: 0 for k in hornos}
            maquinas_fase3 = {k: 0 for k in maquinas_fase3}
            last_product_fase1 = {k: None for k in last_product_fase1}
            last_product_fase2 = {k: None for k in last_product_fase2}
            last_product_fase3 = {k: None for k in last_product_fase3}
            hora_inicio = 0
            hora_limite = 16
            hora_entrega = fin_fase3
            maquina_fase1 = min(maquinas_fase1, key=maquinas_fase1.get)
            setup_time_fase1 = 0.5 if last_product_fase1[maquina_fase1] not in (None, id_producto) else 0
            tiempo_fase1 = cantidad / rates.get((maquina_fase1, id_producto), 0) + setup_time_fase1
            inicio_fase1 = maquinas_fase1[maquina_fase1]
            fin_fase1 = inicio_fase1 + tiempo_fase1
            maquinas_fase1[maquina_fase1] += tiempo_fase1
            last_product_fase1[maquina_fase1] = id_producto
            horno = min(hornos, key=hornos.get)
            setup_time_fase2 = 1 if last_product_fase2[horno] not in (None, id_producto) else 0
            inicio_fase2 = max(hornos[horno], fin_fase1)
            tiempo_fase2 = cantidad / rates.get((horno, id_producto), 0) + setup_time_fase2
            fin_fase2 = inicio_fase2 + tiempo_fase2
            hornos[horno] = fin_fase2
            last_product_fase2[horno] = id_producto
            maquina_fase3 = min(maquinas_fase3, key=maquinas_fase3.get)
            setup_time_fase3 = 0.5 if last_product_fase3[maquina_fase3] not in (None, id_producto) else 0
            inicio_fase3 = max(maquinas_fase3[maquina_fase3], fin_fase2)
            tiempo_fase3 = cantidad / rates.get((maquina_fase3, id_producto), 0) + setup_time_fase3
            fin_fase3 = inicio_fase3 + tiempo_fase3
            maquinas_fase3[maquina_fase3] = fin_fase3
            last_product_fase3[maquina_fase3] = id_producto


        estatus = "a tiempo" if dia_actual <= fecha_limite else "entrega tardia"
        if estatus == "entrega tardia":
            pedidos_tardios += 1

        dia_inicio_nombre = dias_semana[dia_actual % 7]
        hora_inicio_fase1_ajustada = inicio_fase1 + 6
        hora_fin_fase1_ajustada = fin_fase1 + 6
        hora_inicio_fase2_ajustada = inicio_fase2 + 6
        hora_fin_fase2_ajustada = fin_fase2 + 6
        hora_inicio_fase3_ajustada = inicio_fase3 + 6
        hora_fin_fase3_ajustada = fin_fase3 + 6

        def formato_hora(hora):
            return f"{int(hora):02d}:{int((hora % 1) * 60):02d}"

        ruta = {
            "Numero de pedido": pedido['IdExperimento'],
            "Dia de inicio": dia_inicio_nombre,
            "Dia Relativo": dia_actual,
            "Maquina Fase 1": f"Maquina {maquina_fase1}",
            "Fase 1 Hora de Inicio": formato_hora(hora_inicio_fase1_ajustada),
            "Fase 1 Hora de Fin": formato_hora(hora_fin_fase1_ajustada),
            "Horno Fase 2": f"Horno {horno}",
            "Fase 2 Hora de Inicio": formato_hora(hora_inicio_fase2_ajustada),
            "Fase 2 Hora de Fin": formato_hora(hora_fin_fase2_ajustada),
            "Maquina Fase 3": f"Maquina {maquina_fase3}",
            "Fase 3 Hora de Inicio": formato_hora(hora_inicio_fase3_ajustada),
            "Fase 3 Hora de Fin": formato_hora(hora_fin_fase3_ajustada),
            "Dia de salida": dia_inicio_nombre,
            "Hora de salida": formato_hora(hora_fin_fase3_ajustada),
            "Estatus": estatus
        }
        rutas_pedidos.append(ruta)

    return pedidos_tardios, rutas_pedidos

def formato(df):
    df1 = df[["Numero de pedido", "Maquina Fase 1", "Fase 1 Hora de Inicio", "Fase 1 Hora de Fin", "Dia Relativo"]]
    df2 = df[["Numero de pedido", "Horno Fase 2", "Fase 2 Hora de Inicio", "Fase 2 Hora de Fin", "Dia Relativo"]]
    df3 = df[["Numero de pedido", "Maquina Fase 3", "Fase 3 Hora de Inicio", "Fase 3 Hora de Fin", "Dia Relativo"]]
    df4 = df[["Numero de pedido", "Dia de inicio", "Dia de salida", "Hora de salida", "Estatus"]]

    # Renombrar columnas y combinar
    df1_renamed = df1.rename(columns={"Maquina Fase 1": "Máquina", "Fase 1 Hora de Inicio": "Hora de Inicio", "Fase 1 Hora de Fin": "Hora Fin"})
    df1_renamed["Etapa"] = 1

    df2_renamed = df2.rename(columns={"Horno Fase 2": "Máquina", "Fase 2 Hora de Inicio": "Hora de Inicio", "Fase 2 Hora de Fin": "Hora Fin"})
    df2_renamed["Etapa"] = 2

    df3_renamed = df3.rename(columns={"Maquina Fase 3": "Máquina", "Fase 3 Hora de Inicio": "Hora de Inicio", "Fase 3 Hora de Fin": "Hora Fin"})
    df3_renamed["Etapa"] = 3

    df_combined = pd.concat([df1_renamed, df2_renamed, df3_renamed], ignore_index=True)
    df_combined = df_combined[["Numero de pedido", "Máquina", "Etapa", "Hora de Inicio", "Hora Fin", "Dia Relativo"]]
    return df_combined, df4



def iteraciones(df, df2, max_iteraciones=10000):
    mejor_solucion = None
    menor_tardios = float('inf')
    for _ in range(max_iteraciones):
        pedidos_aleatorios = seleccion_aleatoria(df)
        pedidos_tardios, rutas = acomodo(pedidos_aleatorios, df2)
        if pedidos_tardios < menor_tardios:
            menor_tardios = pedidos_tardios
            mejor_solucion = rutas
        if pedidos_tardios == 0:
            break
    print(f"\nMejor solución encontrada con {menor_tardios} pedidos tardíos")
    if mejor_solucion:
        rutas_df = pd.DataFrame(mejor_solucion)
        maquinas, pedidos = formato(rutas_df)
    return maquinas, pedidos, menor_tardios


#Aquí se pueden modificar las rutas de archivos si es necesario. 

pedidos_pocos = "./RATESyPedidos/PEDIDOS1.csv"
pedidos_moderados = "./RATESyPedidos/PEDIDOS2.csv"
pedidos_muchos = "./RATESyPedidos/PEDIDOS3.csv"
pedidos_extremos = "C./RATESyPedidos/PEDIDOS4.csv"

maquinas_pocas = "./RATESyPedidos/RATES1.csv"
maquinas_moderadas = "./RATESyPedidos/RATES2.csv"
maquinas_muchas = "./RATESyPedidos/RATES3.csv"
maquinas_extremas = "./RATESyPedidos/RATES4.csv"

st.title("Programación de Producción")
st.subheader("Con lentitud en la segunda etapa")

st.markdown("##### Casos de Simulación")

option = st.radio("Escoge la opción:", [
    "1. Caso con Pocos Pedidos",
    "2. Caso con Pedidos Moderados",
    "3. Caso con Muchos Pedidos",
    "4. Caso con Pedidos Extremos"
])

if option == "1. Caso con Pocos Pedidos":
    pedidos_creados = pd.read_csv(pedidos_pocos)
    maquinas_creadas = pd.read_csv(maquinas_pocas)
elif option == "2. Caso con Pedidos Moderados":
    pedidos_creados = pd.read_csv(pedidos_moderados)
    maquinas_creadas = pd.read_csv(maquinas_moderadas)
elif option == "3. Caso con Muchos Pedidos":
    pedidos_creados = pd.read_csv(pedidos_muchos)
    maquinas_creadas = pd.read_csv(maquinas_muchas)
elif option == "4. Caso con Pedidos Extremos":
    pedidos_creados = pd.read_csv(pedidos_extremos)
    maquinas_creadas = pd.read_csv(maquinas_extremas)

st.write("Procesando datos...")

maquinas, pedidos, tardios = iteraciones(pedidos_creados, maquinas_creadas, max_iteraciones=1000)

st.markdown("##### Resultados")

st.write("\nMejor solución encontrada con", tardios,"pedidos tardíos")

st.markdown("##### Gráficos de Duración de Pedidos por Día y Etapa")

#Se cambian las horas para los gráficos.
maquinas['Inicio (Horas)'] = maquinas['Hora de Inicio'].str.split(":").str[0].astype(int) + \
                             maquinas['Hora de Inicio'].str.split(":").str[1].astype(int) / 60
maquinas['Fin (Horas)'] = maquinas['Hora Fin'].str.split(":").str[0].astype(int) + \
                          maquinas['Hora Fin'].str.split(":").str[1].astype(int) / 60
maquinas['Duración (Horas)'] = maquinas['Fin (Horas)'] - maquinas['Inicio (Horas)']


unique_dias = maquinas['Dia Relativo'].unique()
color_dict = {1: 'skyblue', 2: 'lightgreen', 3: 'pink'}

# Crear gráficos separados para cada día
for dia in sorted(unique_dias):
    st.write(f"**Día {dia}**")
    df_dia = maquinas[maquinas['Dia Relativo'] == dia]
    
    fig, ax = plt.subplots(figsize=(12, 6))  # Crear figura específica para cada día
    for _, row in df_dia.iterrows():
        ax.barh(
            row['Numero de pedido'],  # Eje Y: Pedido ID
            row['Duración (Horas)'],  # Longitud de la barra
            left=row['Inicio (Horas)'],  # Inicio de la barra
            color=color_dict[row['Etapa']]  # Color basado en la etapa
        )
        numero_maquina = row['Máquina'].split()[-1]
        ax.text(
            row['Fin (Horas)']-0.1,  # Posición X un poco después del final de la barra
            row['Numero de pedido'],  # Posición Y es el pedido ID
            f"{numero_maquina}",  # Mostrar solo el número de la máquina
            va='center',  # Alinear verticalmente con el centro de la barra
            fontsize=12, color='black'  # Opciones de estilo
        )


    ax.set_title(f"Duración de los pedidos - Día {dia}", fontsize=14)
    ax.set_xlabel("Tiempo (Horas del Día)", fontsize=12)
    ax.set_ylabel("Pedido ID", fontsize=12)
    ax.set_xlim(6, 22)  # Ejemplo: Limitar entre 6 AM y 10 PM
    plt.tight_layout()
    
    plt.legend(
        handles=[
            plt.Line2D([0], [0], color='skyblue', lw=4, label='Etapa 1'),
            plt.Line2D([0], [0], color='lightgreen', lw=4, label='Etapa 2'),
            plt.Line2D([0], [0], color='pink', lw=4, label='Etapa 3')
        ],
        loc='upper right',
        title="Significado de los Colores"
    )

    # Mostrar gráfico en Streamlit
    st.pyplot(fig)


st.write("**Horario por Máquina y Etapa**")
maquinas = maquinas.iloc[:, :-3]
st.dataframe(maquinas)

buffer = BytesIO()
maquinas.to_csv(buffer, index=False, encoding='utf-8')
buffer.seek(0)
        
st.download_button(
    label="Descargar horarios en CSV",
    data=buffer,
    file_name="horarios_maquinas.csv",
    mime="text/csv")

st.write("**Resumen de Pedidos**")
st.dataframe(pedidos)

buffer = BytesIO()
pedidos.to_csv(buffer, index=False, encoding='utf-8')
buffer.seek(0)
        
st.download_button(
    label="Descargar horarios en CSV",
    data=buffer,
    file_name="horarios_pedidos.csv",
    mime="text/csv")
