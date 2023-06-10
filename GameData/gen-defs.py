import sys
import os
import dbf

dbf_path = "./ne2_10m/ne_10m_admin_1_states_provinces.dbf"
countries_path = "./history/countries"
countries_file_len = len([name for name in os.listdir(countries_path) if os.path.isfile(os.path.join(countries_path, name))])

states_path = "./map/states"
states_file_len = len([name for name in os.listdir(states_path) if os.path.isfile(os.path.join(states_path, name))])

if countries_file_len > 0:
    print(f"Warning: Directory \"{countries_path}\" was not empty.")
    exit(1)

if states_file_len > 0:
    print(f"Warning: Directory \"{states_path}\" was not empty.")
    exit(1)


table = dbf.Table(filename=dbf_path, codepage="utf8")
table.open(dbf.READ_ONLY)

countries = []
country = {}
country["provinces"] = []

for record in table:
    try:
        pid = str(record["adm1_code"]).strip()
        cid = str(record["adm0_a3"]).strip()
        cname = str(record["admin"]).strip()
        pname = str(record["name"]).strip().replace("/", "_")
        region = str(record["region"]).strip().replace("/", "_")

    except:
        print(record)
        continue

    if len(country.keys()) > 1 and country["cid"] != cid:
        overwrote = False
        for (i, c) in enumerate(countries):
            if c["cid"] == country["cid"]:
                countries[i]["provinces"] += country["provinces"]
                overwrote = True
                break

        if not overwrote:
            countries.append(country)

        country = {}
        country["provinces"] = []

    province = {}
    province["pid"] = pid
    province["region"] = region
    province["name"] = pname

    country["provinces"].append(province)

    if len(country.keys()) == 1:
        country["cid"] = cid
        country["name"] = cname

table.close()

# generate definitions
sid = 0

for c in countries:
    provinces = c["provinces"]

    cid = c["cid"]
    pid = list(map(lambda x: x["pid"], provinces))
    cname = c["name"]

    states = []
    state = {}
    state["provinces"] = []
    state["id"] = sid

    for p in provinces:
        region = str(p["region"])

        if len(state.keys()) == 3 and state["name"] != region and state["name"] != p["region"]:
            overwrote = False
            for (i, s) in enumerate(states):
                if s["name"] == state["name"]:
                    states[i]["provinces"] += state["provinces"]
                    overwrote = True
                    break

            if not overwrote:
                states.append(state)

            sid += 1
            state = {}
            state["provinces"] = []
            state["id"] = sid

        state["provinces"].append(p["pid"])

        if len(state.keys()) == 2:
            if region == "":
                region = p["name"]

            state["name"] = region

    for s in states:
        state_id = s["id"]
        name = s["name"]
        province_ids = str(s["provinces"]).replace("[", "{").replace("]", "}").replace("\'", "\"")
        filename = f"{states_path}/{state_id}-{name}.lua"

        txt = f"id = \"{state_id}\"\nprovinces = {province_ids}\nname = \"{name}\""

        f = open(filename, "w")
        f.write(txt)
        f.close()


    state_ids = str(list(map(lambda x: str(x["id"]), states))).replace("[", "{").replace("]", "}").replace("\'", "\"")
    filename = f"{countries_path}/{cid}-{cname}.lua"
    txt = f"id = \"{cid}\"\nname = \"{cname}\"\nstates = {state_ids}\n"

    f = open(filename, "w")
    f.write(txt)
    f.close()